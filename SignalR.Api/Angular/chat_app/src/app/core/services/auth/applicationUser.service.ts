import { Injectable, model } from "@angular/core";
import { Observable } from "rxjs";
import { GetUserDto } from "../../models/common/getUserDto";
import { IdNameDto } from "../../models/common/idNameDto";
import { ResultDto } from "../../models/common/resultDto";
import { LogInDto } from "../../models/auth/logInDto";
import { HttpClientService } from "../common/http-client.service";

@Injectable({
    providedIn: "root"
})
export class ApplicationUserService {
    private usersUrl = 'user/';
    constructor(private httpClient: HttpClientService) { }
    /**
    /// get all users inside db
    /// </summary>
    */
    getUsers(): Observable<GetUserDto[]> {
        return this.httpClient.get(this.usersUrl + 'getUsers');
    }

    /**
    /// get groups of a user
    /// </summary>
    /// <param name="userId"></param>
    */
    getUserGroups(userId: number): Observable<IdNameDto[]> {
        return this.httpClient.get(this.usersUrl + 'getUserGroups?userId=' + userId);
    }

    /**
    /// log in
    /// </summary>
    /// <param name="model"></param>
    */
    signIn(model: LogInDto): Observable<ResultDto<GetUserDto>> {
        return this.httpClient.post(this.usersUrl + 'signIn', model);
    }

    signOut(): Observable<unknown> {
        return this.httpClient.post(this.usersUrl + 'SignOut', {});
    }

}