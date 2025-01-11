import { Observable } from "rxjs"
import { GetGroupDto } from "../../models/chats/addGroupDto"
import { HttpClientService } from "../common/http-client.service";
import { RequestUserChatMessageDto, GetUserChatMessageDto } from "../../models/chats/userChatDto";
import { ResultDto } from "../../models/common/resultDto";
import { Injectable } from "@angular/core";

@Injectable({
    providedIn: "root"
})
export class UserChatService {
    private userChatUrl = 'userChat/';
    constructor(private httpClient: HttpClientService) { }
    /**
    *get user chats or groups that he is a member of it.
    *</summary>
    */
    getUserChatsWithGroups(): Observable<GetGroupDto[]> {
        return this.httpClient.get(this.userChatUrl + 'getUserChatsWithGroups');
    }

    /**
    *pagination chat messages by id
    *</summary>
    *<param name="model"></param>
    */
    getUserChatMessages(model: RequestUserChatMessageDto): Observable<GetUserChatMessageDto[]> {
        var url = this.userChatUrl + 'getUserChatMessages';
        url += '?length=' + model.length + '&pageNumber=' + model.pageNumber
            + '&userChatId=' + model.userChatId;
        return this.httpClient.get(url);
    }

    /**
    *set message as seen
    *</summary>
    *<param name="userChatId"></param>
    */
    messagesSeen(userChatId: number): Observable<ResultDto<object>> {
        return this.httpClient.post(this.userChatUrl + 'messagesSeen?userChatId=' + userChatId, {});
    }
}