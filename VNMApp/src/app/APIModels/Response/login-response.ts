export class LoginResponse {
    AccessToken:string;
    RefreshToken:string;
    Expiration:string;

    constructor(AccessToken?:string, RefreshToken?:string,Expiration?:string) {
        this.AccessToken = AccessToken||'';
        this.RefreshToken = RefreshToken||'';
        this.Expiration = Expiration||'';
      }
}
