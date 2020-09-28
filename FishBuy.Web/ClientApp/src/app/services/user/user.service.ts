import { Injectable, Inject } from "@angular/core";
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Observable } from "rxjs";
import { User } from "src/app/model/user";

@Injectable({
  providedIn: "root",
})
export class UserService {
  private baseUrl: string;
  private _user: User;

  constructor(private http: HttpClient, @Inject("BASE_URL") baseUrl: string) {
    this.baseUrl = baseUrl;
  }

  set user(user: User) {
    sessionStorage.setItem("user-authenticated", JSON.stringify(user));
    this._user = user;
  }

  get user(): User {
    let user_json = sessionStorage.getItem("user-authenticated");
    this._user = JSON.parse(user_json);
    return this._user;
  }

  public user_authenticated(): boolean {
    return (
      this._user != undefined &&
      this._user.email != "" &&
      this._user.token != ""
    );
  }

  public user_admin(): boolean {
    return this.user_authenticated() && this.user.administrator;
  }

  public clear_session() {
    sessionStorage.setItem("user-authenticated", "");
    sessionStorage.setItem("authenticationToken", "");
    this._user = undefined;
  }

  get headers() {
    return new HttpHeaders().set("content-type", "application/json");
  }

  public getAllUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + "api/user");
  }

  public verifyUser(user: User): Observable<User> {
    const headers = new HttpHeaders().set("content-type", "application/json");
    var body = {
      email: user.email,
      password: user.password,
    };
    return this.http.post<User>(
      this.baseUrl + "api/user/authentication",
      body,
      {
        headers,
      }
    );
  }

  public register(user: User): Observable<User> {
    return this.http.post<User>(
      this.baseUrl + "api/user/registration",
      JSON.stringify(user),
      {
        headers: this.headers,
      }
    );
  }

  public update(user: User): Observable<User> {
    return this.http.put<User>(
      this.baseUrl + "api/user",
      JSON.stringify(user),
      {
        headers: this.headers,
      }
    );
  }

  public delete(user: User): Observable<User[]> {
    return this.http.delete<User[]>(this.baseUrl + `api/user/${user.id}`, {
      headers: this.headers,
    });
  }
}
