export class LoginRequest {
   public UserName:string;
   public Password:string;

    constructor(UserName?:string, Password?:string) {
        this.UserName = UserName||'';
        this.Password = Password||'';
      }

       isValid(): boolean {
      return this.UserName.length > 0 && this.Password.length >= 6; 
      }
}
