namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class LoginResponseDto {
        public string Access_token { get; set; }
        public string Exprires_in { get; set; }
        public string Email { get; set; }
        public string Token_type { get; set; }
        public string Message { get; set; }
        public string Error_code { get; set; }

    }
}
