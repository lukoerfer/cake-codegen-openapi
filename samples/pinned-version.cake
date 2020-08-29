#r "../src/Cake.OpenApiGenerator/bin/Debug/netstandard2.0/Cake.OpenApiGenerator.dll"

Task("Generate")
	.Does(() =>
{
	OpenApiGenerator["3.3.4"].Generate(new OpenApiGeneratorGenerateSettings() {
		Specification = "./petstore.json",
		Generator = "csharp",
		OutputDirectory = "./src"
	});
});

RunTarget("Generate");