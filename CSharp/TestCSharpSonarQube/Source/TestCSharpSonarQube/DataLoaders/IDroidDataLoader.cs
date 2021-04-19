namespace TestCSharpSonarQube.DataLoaders
{
    using System;
    using TestCSharpSonarQube.Models;
    using GreenDonut;

    public interface IDroidDataLoader : IDataLoader<Guid, Droid>
    {
    }
}
