import { AfterViewInit, ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { ChatUsersComponent } from "../chat-users/chat-users.component";
import { ChatBoxComponent } from "../chat-box/chat-box.component";
import { ChatBoxSendMessageComponent } from "../chat-box-send-message/chat-box-send-message.component";
import { GetGroupDto } from '../../../models/chats/addGroupDto';
import { SignalRService } from '../../../services/common/signalr.service';
import { HubConnection, HubConnectionState } from '@microsoft/signalr';
import { CommonModule } from '@angular/common';
import { UserChatService } from '../../../services/chats/userChat.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { BaseComponent } from '../../common/basComponent';
import { GetUserChatMessageDto, NotifyUserMessageDto } from '../../../models/chats/userChatDto';

@Component({
  standalone: true,
  selector: 'app-home',
  imports: [ChatUsersComponent, ChatBoxComponent, ChatBoxSendMessageComponent,
    CommonModule
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent extends BaseComponent implements OnInit {
  hubConnection?: HubConnection | undefined;
  /**messages of current active chat */
  chatMessages: GetUserChatMessageDto[] = [];
  /**active chat */
  activeUserChat?: GetGroupDto;
  @ViewChild(ChatBoxComponent) chatBoxComponent!: ChatBoxComponent;
  @ViewChild(ChatUsersComponent) chatUsersComponent!: ChatUsersComponent;
  constructor(private signalRService: SignalRService, private userChatService: UserChatService
  ) {
    super();
  }

  public get IsSignalrConnected(): boolean {
    return this.hubConnection != null && this.hubConnection.state == HubConnectionState.Connected;
  }

  setActiveChat(chat: GetGroupDto) {
    this.activeUserChat = chat;
    this.chatMessages = [];
    if (chat.id != 0) {// 0 means new chat
      this.userChatService.getUserChatMessages({
        length: 30,
        pageNumber: 1,
        userChatId: chat.id
      }).pipe(takeUntilDestroyed(this.destroyRef))
        .subscribe({
          next: (res) => {
            this.chatMessages = res;
            this.chatBoxComponent.scrollToEnd();
          }
        });
    }
  }

  //#region signal r events and functions

  ngOnInit(): void {
    this.signalRService.startConnection();
    this.signalRService.onStart((hub) => { this.onSignalrRStart(hub); });
    this.signalRService.onClose((err) => { this.onSignalrRClose(err) });
    this.signalRService.onMessageAdded((model) => { this.onRecieveMessage(model) });
    this.signalRService.onNewOnlineUser((userId) => {
      this.chatUsersComponent.onNewOnlineOfflineUser(userId, true);
    });
    this.signalRService.onOfflineUser((userId) => {
      console.log('off');
      this.chatUsersComponent.onNewOnlineOfflineUser(userId, false);
    });
  }

  /**
   * on signalr r connected successfully
   * @param hubConnection 
   */
  onSignalrRStart(hubConnection: HubConnection) {
    this.hubConnection = hubConnection;
  }

  /**
   * on signal r dis connected
   * @param err 
   */
  onSignalrRClose(err?: Error) {
    this.hubConnection = undefined;
  }

  /**
   * current user recieves a message
   * @param model 
   */
  onRecieveMessage(model: NotifyUserMessageDto) {
    if (this.activeUserChat != null && this.activeUserChat.id == model.userChatId) {
      //current chat is the same chat of sent message
      //notify chat box
      this.chatMessages.push(this.notifyMessageAsChatMessage(model))
      this.chatBoxComponent.scrollToEnd();
    }
    //update users chats (aside bar by the last message)
    this.chatUsersComponent.onRecieveMessage(model);
  }


  /**
   * this is commig from current user that he sent a message from the text box and clicked the button
   * @param message 
   */
  onUserSendNewMessage(message: GetUserChatMessageDto) {
    this.chatMessages.push(message);
    this.chatBoxComponent.scrollToEnd();
    this.chatUsersComponent.onRecieveMessage(null);
  }

  //#endregion

  //#region Helpers functions
  private notifyMessageAsChatMessage(model: NotifyUserMessageDto): GetUserChatMessageDto {
    return {
      createdDate: model.createdDate,
      from: model.fromFullName,
      isFromMe: false,
      isSeen: true,
      message: model.message,
      to: ''
    };
  }
  //#endregion
}
