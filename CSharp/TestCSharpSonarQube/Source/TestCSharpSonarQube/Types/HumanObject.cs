namespace TestCSharpSonarQube.Types
{
    using TestCSharpSonarQube.Constants;
    using TestCSharpSonarQube.Models;
    using TestCSharpSonarQube.Resolvers;
    using HotChocolate.Types;

    public class HumanObject : ObjectType<Human>
    {
        protected override void Configure(IObjectTypeDescriptor<Human> descriptor)
        {
            descriptor
                .Name("Human")
                .Description("A humanoid creature from the Star Wars universe.")
                .Implements<CharacterInterface>();

            // descriptor.Authorize(AuthorizationPolicyName.Admin); // To require authorization for all fields in this type.
            descriptor
                .ImplementsNode()
                .IdField(x => x.Id)
                .ResolveNodeWith<HumanResolver>(x => x.GetHumanAsync(default!, default!, default!));
            descriptor
                .Field(x => x.Name)
                .Description("The name of the human.");
            descriptor
                .Field(x => x.DateOfBirth)
                .Authorize(AuthorizationPolicyName.Admin) // Require authorization to access the date of birth field.
                .Description("The humans date of birth.");
            descriptor
                .Field(x => x.HomePlanet)
                .Description("The home planet of the human.");
            descriptor
                .Field(x => x.AppearsIn)
                .Description("Which movie they appear in.");
            descriptor
                .Field(x => x.Friends)
                .Type<NonNullType<ListType<NonNullType<CharacterInterface>>>>()
                .Description("The friends of the character, or an empty list if they have none.")
                .ResolveWith<HumanResolver>(x => x.GetFriendsAsync(default!, default!, default!));
        }
    }
}
