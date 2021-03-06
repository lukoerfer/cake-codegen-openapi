﻿using Cake.Core.IO;
using Cake.OpenApiGenerator.Util;
using Cake.Testing;
using FakeItEasy;
using NUnit.Framework;

using System.IO;
using System.Text;

namespace Cake.OpenApiGenerator.Maven
{
    [TestFixture]
    class MavenClientTest
    {
        private FakeFileSystem fileSystem;
        private DirectoryPath localRepository;
        private IWebClient remoteRepository;

        [SetUp]
        public void Setup()
        {
            var environment = FakeEnvironmentHelper.CreateFromRuntime();
            fileSystem = new FakeFileSystem(environment);
            localRepository = new DirectoryPath(".m2");
            remoteRepository = A.Fake<IWebClient>();

            var metadata = Encoding.UTF8.GetBytes("<metadata><versioning><latest>2.0.0</latest></versioning></metadata>");
            A.CallTo(() => remoteRepository.OpenRead(A<string>.That.EndsWith(".xml"))).ReturnsLazily(_ => new MemoryStream(metadata));
        }

        [Test]
        public void Resolve_PackageDoesNotExist_ReadsPackageFromRemoteRepository()
        {
            var mavenClient = new MavenClient(fileSystem, localRepository, remoteRepository);

            mavenClient.Resolve(new MavenPackage("group", "artifact", "1.0.0"));

            A.CallTo(() => remoteRepository.OpenRead(A<string>.That.EndsWith(".jar"))).MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Resolve_PackageExists_DoesNotReadFromRemoteRepository()
        {
            var localPackage = new FilePath("group/artifact/1.0.0/artifact-1.0.0.jar");
            fileSystem.CreateFile(localRepository.CombineWithFilePath(localPackage));
            var mavenClient = new MavenClient(fileSystem, localRepository, remoteRepository);

            mavenClient.Resolve(new MavenPackage("group", "artifact", "1.0.0"));

            A.CallTo(() => remoteRepository.OpenRead(A<string>._)).MustNotHaveHappened();
        }

        [Test]
        public void Resolve_PackageVersionUndefined_QueriesMetadata()
        {
            var mavenClient = new MavenClient(fileSystem, localRepository, remoteRepository);

            mavenClient.Resolve(new MavenPackage("group", "artifact", version: null));

            A.CallTo(() => remoteRepository.OpenRead(A<string>.That.EndsWith(".xml"))).MustHaveHappenedOnceExactly();
        }
    }
}
