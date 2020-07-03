﻿using System;
using System.Collections.Generic;
using System.Linq;

using Cake.Core;
using Cake.Core.IO;

namespace Cake.OpenApiGenerator.Settings
{
    /// <summary>
    /// Stores settings for the OpenAPI generator command <c>generate</c>
    /// </summary>
    public class OpenApiGenerateSettings : OpenApiBaseSettings
    {
        public override string Command => "generate";

        /// <summary>
        /// Gets or sets the OpenAPI specification file
        /// </summary>
        /// <remarks>This parameter is required.</remarks>
        public FilePath SpecificationFile { get; set; }

        /// <summary>
        /// Gets or sets the generator
        /// </summary>
        /// <remarks>This parameter is required.</remarks>
        public string Generator { get; set; }

        /// <summary>
        /// Gets or sets the output directory
        /// </summary>
        /// <remarks>This parameter is required.</remarks>
        public DirectoryPath OutputDirectory { get; set; }

        /// <summary>
        /// Gets or sets the path to a configuration file with generator-specific properties
        /// </summary>
        /// <remarks>If this parameter is defined, <see cref="AdditionalProperties"/> will be ignored</remarks>
        public FilePath ConfigurationFile { get; set; }

        /// <summary>
        /// Gets or sets generator-specific properties
        /// </summary>
        /// <remarks>If <see cref="ConfigurationFile"/> is defined, this parameter will be ignored</remarks>
        public Dictionary<string, string> AdditionalProperties { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public string Authorization { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApiNameSuffix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ApiPackage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ArtifactId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ArtifactVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool DryRun { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TemplatingEngine { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool EnablePostProcessFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool GenerateAliasAsModel { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GitHost { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GitRepository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string GitUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> GlobalProperties { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string HttpUserAgent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public FilePath IgnoreFile { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> ImportMappings { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public List<string> InstantiationTypes { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public string InvokerPackage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<string> LanguageSpecificPrimitives { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public string Library { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool LogToStandardError { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool MinimalUpdate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModelNamePrefix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModelNameSuffix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ModelPackage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ReleaseNote { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool RemoveOperationIdPrefix { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> ReservedWordsMappings { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> ServerVariables { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public bool SkipOverwrite { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool SkipValidation { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool StrictSpec { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DirectoryPath TemplateDirectory { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> TypeMappings { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// 
        /// </summary>
        public bool Verbose { get; set; }

        protected override void ApplyParameters(ProcessArgumentBuilder args)
        {
            if (SpecificationFile == null)
                throw new ArgumentNullException(nameof(SpecificationFile));
            if (Generator == null)
                throw new ArgumentNullException(nameof(Generator));
            if (OutputDirectory == null)
                throw new ArgumentNullException(nameof(OutputDirectory));

            args.Append("-i").Append(SpecificationFile.FullPath);
            args.Append("-g").Append(Generator);
            args.Append("-o").Append(OutputDirectory.FullPath);

            if (ConfigurationFile != null)
            {
                args.Append("-c").Append(ConfigurationFile.FullPath);
            }
            else if (AdditionalProperties != null && AdditionalProperties.Count > 0)
            {
                args.Append("--additional-properties=" + string.Join(",", AdditionalProperties.Select(e => e.Key + "=" + e.Value)));
            }

            if (Authorization != null)
            {
                args.Append("-a").Append(Authorization);
            }
            if (ApiNameSuffix != null)
            {
                args.Append("--api-name-suffix").Append(ApiNameSuffix);
            }
            if (ApiPackage != null)
            {
                args.Append("--api-package").Append(ApiPackage);
            }
            if (ArtifactId != null)
            {
                args.Append("--artifact-id").Append(ArtifactId);
            }
            if (ArtifactVersion != null)
            {
                args.Append("--artifact-version").Append(ArtifactVersion);
            }
            if (DryRun)
            {
                args.Append("--dry-run");
            }
            if (TemplatingEngine != null)
            {
                args.Append("-e").Append(TemplatingEngine);
            }
            if (EnablePostProcessFile)
            {
                args.Append("--enable-post-process-file");
            }
            if (GenerateAliasAsModel)
            {
                args.Append("--generate-alias-as-model");
            }
            if (GitHost != null)
            {
                args.Append("--git-host").Append(GitHost);
            }
            if (GitRepository != null)
            {
                args.Append("--git-repo-id").Append(GitRepository);
            }
            if (GitUser != null)
            {
                args.Append("--git-user-id").Append(GitUser);
            }
            if (GlobalProperties != null && GlobalProperties.Count > 0)
            {
                args.Append("--global-properties").Append(string.Join(",", GlobalProperties.Select(e => e.Key + "=" + e.Value)));
            }
            if (GroupId != null)
            {
                args.Append("--group-id").Append(GroupId);
            }
            if (HttpUserAgent != null)
            {
                args.Append("--http-user-agent").Append(HttpUserAgent);
            }
            if (IgnoreFile != null)
            {
                args.Append("--ignore-file-override").Append(IgnoreFile.FullPath);
            }
            if (ImportMappings != null && ImportMappings.Count > 0)
            {
                args.Append("--import-mappings=" + string.Join(",", ImportMappings.Select(e => e.Key + "=" + e.Value)));
            }
            if (InstantiationTypes != null && InstantiationTypes.Count > 0)
            {
                args.Append("--instantiation-types").Append(string.Join(",", InstantiationTypes));
            }
            if (InvokerPackage != null)
            {
                args.Append("--invoker-package").Append(InvokerPackage);
            }
            if (LanguageSpecificPrimitives != null && LanguageSpecificPrimitives.Count > 0)
            {
                args.Append("--language-specific-primitives").Append(string.Join(",", LanguageSpecificPrimitives));
            }
            if (Library != null)
            {
                args.Append("--library").Append(Library);
            }
            if (LogToStandardError)
            {
                args.Append("--log-to-stderr");
            }
            if (MinimalUpdate)
            {
                args.Append("--minimal-update");
            }
            if (ModelNamePrefix != null)
            {
                args.Append("--model-name-prefix").Append(ModelNamePrefix);
            }
            if (ModelNameSuffix != null)
            {
                args.Append("--model-name-suffix").Append(ModelNameSuffix);
            }
            if (ModelPackage != null)
            {
                args.Append("--model-package").Append(ModelPackage);
            }
            if (PackageName != null)
            {
                args.Append("--package-name").Append(PackageName);
            }
            if (ReleaseNote != null)
            {
                args.Append("--release-note").Append(ReleaseNote);
            }            
            if (RemoveOperationIdPrefix)
            {
                args.Append("--remove-operation-id-prefix");
            }
            if (ReservedWordsMappings != null && ReservedWordsMappings.Count > 0)
            {
                args.Append("--reserved-word-mappings=" + string.Join(",", ReservedWordsMappings.Select(e => e.Key + "=" + e.Value)));
            }
            if (SkipOverwrite)
            {
                args.Append("-s");
            }
            if (SkipValidation)
            {
                args.Append("--skip-validate-spec");
            }
            if (TemplateDirectory != null)
            {
                args.Append("-t").Append(TemplateDirectory.FullPath);
            }
            if (TypeMappings != null && TypeMappings.Count > 0)
            {
                args.Append("--type-mappings=" + string.Join(",", TypeMappings.Select(e => e.Key + "=" + e.Value)));
            }
            if (Verbose)
            {
                args.Append("-v");
            }
        }

    }
}
