namespace APITraining.Session3.Resources;

public class Endpoints
{

    public const string BaseURL = "https://petstore.swagger.io/v2/";

    public static string PetEndpoint = "pet";

    public static string FindOrDeletePetById(long petId) => $"{BaseURL}{PetEndpoint}/{petId}";

    public static string CreateOrUpdatePet() => $"{BaseURL}{PetEndpoint}";

}
