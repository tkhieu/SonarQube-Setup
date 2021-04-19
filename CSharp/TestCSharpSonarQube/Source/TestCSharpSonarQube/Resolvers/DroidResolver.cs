namespace TestCSharpSonarQube.Resolvers
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using TestCSharpSonarQube.DataLoaders;
    using TestCSharpSonarQube.Models;
    using TestCSharpSonarQube.Repositories;
    using HotChocolate;

    public class DroidResolver
    {
        public Task<Droid> GetDroidAsync(IDroidDataLoader droidDataLoader, Guid id, CancellationToken cancellationToken) =>
            droidDataLoader.LoadAsync(id, cancellationToken);

        public Task<List<Character>> GetFriendsAsync(
            [Service] IDroidRepository droidRepository,
            [Parent] Droid droid,
            CancellationToken cancellationToken) =>
            droidRepository.GetFriendsAsync(droid, cancellationToken);
    }
}
