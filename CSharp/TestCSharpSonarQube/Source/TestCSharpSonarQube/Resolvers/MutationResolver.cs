namespace TestCSharpSonarQube.Resolvers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Boxed.Mapping;
    using TestCSharpSonarQube.Models;
    using TestCSharpSonarQube.Repositories;
    using HotChocolate;

    public class MutationResolver
    {
        public async Task<Human> CreateHumanAsync(
            [Service] IImmutableMapper<HumanInput, Human> humanInputToHumanMapper,
            [Service] IHumanRepository humanRepository,
            HumanInput humanInput,
            CancellationToken cancellationToken)
        {
            var human = humanInputToHumanMapper.Map(humanInput);
            human = await humanRepository
                .AddHumanAsync(human, cancellationToken)
                .ConfigureAwait(false);
            return human;
        }
    }
}
