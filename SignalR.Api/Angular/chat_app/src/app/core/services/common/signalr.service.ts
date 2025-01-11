import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';
import { HttpClientService } from './http-client.service';
import { GetGroupDto } from '../../models/chats/addGroupDto';
import { NotifyUserMessageDto } from '../../models/chats/userChatDto';
import { AuthenticateService } from '../auth/authenticate.service';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    constructor(private authenticateService: AuthenticateService) {

    }
    private hubConnection!: signalR.HubConnection;
    private signalrConfiguration = {
        route: 'chatHub',
        addedToGroup: 'AddedToGroup',
        messageAdded: 'MessageAdded',
        newOnlineUser: 'NewOnlineUser',
        offlineUser: "OfflineUser"
    };
    onStartFunction?: (connection: signalR.HubConnection) => void;
    public startConnection(): Promise<void> {
        this.hubConnection = new signalR.HubConnectionBuilder()
            .withUrl(HttpClientService.domain + this.signalrConfiguration.route,
                {
                    withCredentials: true,
                    //as query string named access_token
                    accessTokenFactory: () => this.authenticateService.getToken()
                }
            )
            .withAutomaticReconnect()
            .withHubProtocol(new signalR.JsonHubProtocol())
            .configureLogging(signalR.LogLevel.Information)
            // .withKeepAliveInterval(1500)
            .build();

        return this.hubConnection
            .start()
            .then(() => {
                setInterval(() => {
                    //update user status every 50 seconds
                    if (this.hubConnection.state == signalR.HubConnectionState.Connected) {
                        this.hubConnection.invoke("UpdateUserStatus");
                    }
                }, 50000);
                if (this.hubConnection.state == signalR.HubConnectionState.Connected) {
                    if (this.onStartFunction != null)
                        this.onStartFunction(this.hubConnection);
                }
            })
            .then(() => console.log('Connection started'))
            .catch(err => console.log('Error while starting connection: ' + err))
    }

    public onStart(func: (connection: signalR.HubConnection) => void) {
        this.onStartFunction = func;
    }

    public onClose(func: (err?: Error) => void) {
        this.hubConnection.onclose(func);
    }

    public onAddedToGroup(func: (model: GetGroupDto) => void) {
        //receiving a message from a group
        this.hubConnection.on(this.signalrConfiguration.addedToGroup, func);
    }


    public onMessageAdded(func: (model: NotifyUserMessageDto) => void) {
        //receiving a message from a group
        this.hubConnection.on(this.signalrConfiguration.messageAdded, func);
    }

    public onNewOnlineUser(func: (model: number) => void) {
        //who's become online ?
        this.hubConnection.on(this.signalrConfiguration.newOnlineUser, func);
    }
    public onOfflineUser(func: (model: number) => void) {
        //who's become online ?
        this.hubConnection.on(this.signalrConfiguration.offlineUser, func);
    }
}
