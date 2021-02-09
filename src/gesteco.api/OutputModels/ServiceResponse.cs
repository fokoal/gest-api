namespace gesteco.api.src.gesteco.WebApi.OutputModels {
    public class ServiceResponse <T>{

        public T Data { get; set; }

        public bool Success { get; set; } = true;

        public string Messages { get; set; } = null;
    }
}
