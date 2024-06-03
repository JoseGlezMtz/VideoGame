public class LoginResponse{
    public int user_id;
    public bool loginSuccesful;
    public string message;

    public bool GetLoginResponse(){
        return loginSuccesful;
    }
}