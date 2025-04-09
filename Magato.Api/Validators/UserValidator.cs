public static class UserRegisterValidator
{
    public static bool IsValid(UserRegisterDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Username)
            && !string.IsNullOrWhiteSpace(dto.Password)
            && dto.Password.Length >= 6;
    }
}
