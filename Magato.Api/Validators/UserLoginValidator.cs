public static class UserLoginValidator
{
    public static bool IsValid(UserLoginDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Username)
            && !string.IsNullOrWhiteSpace(dto.Password);
    }
}
