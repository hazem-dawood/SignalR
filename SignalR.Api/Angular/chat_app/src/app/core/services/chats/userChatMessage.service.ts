import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AddMessageDto } from "../../models/chats/userChatDto";
import { ResultDto } from "../../models/common/resultDto";
import { HttpClientService } from "../common/http-client.service";

@Injectable({
    providedIn: "root"
})
export class UserChatMessageService {
    private userChatMessageUrl = 'userChatMessage/';
    constructor(private httpClient: HttpClientService) { }
    /** 
    * send new message
    * </summary>
    * <param name="model"></param>
    */
    add(model: AddMessageDto): Observable<ResultDto<AddMessageDto>> {
        return this.httpClient.post(this.userChatMessageUrl + 'add', model);
    }
}