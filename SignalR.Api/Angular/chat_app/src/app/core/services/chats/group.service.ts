import { Injectable } from "@angular/core";
import { HttpClientService } from "../common/http-client.service";
import { Observable } from "rxjs";
import { ResultDto } from "../../models/common/resultDto";
import { AddGroupDto, EditGroupDto, GetGroupDto, RequestGroupMessageDto, SendGroupMessageDto } from "../../models/chats/addGroupDto";
import { GetUserChatMessageDto } from "../../models/chats/userChatDto";

@Injectable({
    providedIn: "root"
})
export class GroupService {
    private groupUrl = 'group/';
    constructor(private httpClient: HttpClientService) { }
    /**
     * add new group
     * @param model 
     * @returns 
     */
    Add(model: AddGroupDto): Observable<ResultDto<EditGroupDto>> {
        return this.httpClient.post(this.groupUrl + 'add', model);
    }

    /** 
    * get the groups that the current user is a member of it.
    * </summary>
    */
    getCurrentUserGroups(): Observable<ResultDto<GetGroupDto[]>> {
        return this.httpClient.get(this.groupUrl + 'getCurrentUserGroups');
    }

    /**
    * pagination group messages by group id
    */
    getGroupMessages(model: RequestGroupMessageDto): Observable<GetUserChatMessageDto[]> {
        var url = this.groupUrl + 'getGroupMessages?';
        url += 'groupId=' + model.groupId + '&length=' + model.length + '&pageNumber=' + model.pageNumber;
        return this.httpClient.get(url);
    }

    /**
    * send a message to a group
   */
    sendMessage(model: SendGroupMessageDto): Observable<ResultDto<object>> {
        return this.httpClient.post(this.groupUrl + 'sendMessage', model);
    }
}