import { ChangeDetectorRef, Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { ChatUserComponent } from "./chat-user/chat-user.component";
import { ChatGroupComponent } from "./chat-group/chat-group.component";
import { ApplicationUserService } from '../../../services/auth/applicationUser.service';
import { UserChatService } from '../../../services/chats/userChat.service';
import { BaseComponent } from '../../common/basComponent';
import { GetGroupDto } from '../../../models/chats/addGroupDto';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { GetUserDto } from '../../../models/common/getUserDto';
import { NotifyUserMessageDto } from '../../../models/chats/userChatDto';

@Component({
  standalone: true,
  selector: 'app-chat-users',
  imports: [ChatUserComponent, ChatGroupComponent],
  templateUrl: './chat-users.component.html',
  styleUrl: './chat-users.component.css'
})
export class ChatUsersComponent extends BaseComponent implements OnInit {
  activeUserChat?: GetGroupDto;
  @Output()
  setActiveToParent = new EventEmitter<GetGroupDto>();
  public get allChats(): GetGroupDto[] {
    var userChast: GetGroupDto[] = this.chats;
    if (this.users.length > 0) {
      var usersWithNoChast = this.users.filter(a => this.isUserExists(a) == false);
      for (let index = 0; index < usersWithNoChast.length; index++) {
        const element = usersWithNoChast[index];
        userChast.push(this.userAsChatUserObject(element));
      }
    }
    return userChast;
  }

  chats: GetGroupDto[] = [];
  users: GetUserDto[] = [];
  constructor(private applicationUserService: ApplicationUserService,
    private userChatService: UserChatService
  ) {
    super();
  }

  ngOnInit(): void {
    this.getUsers();
    this.updateChats();
  }

  setActive(chat: GetGroupDto) {
    this.activeUserChat = chat;
    this.setActiveToParent.emit(chat);
  }

  /**
   * //update users chats (aside bar by the last message)
   * @param model 
   */
  onRecieveMessage(model: NotifyUserMessageDto | null) {
    // it will be good if you reorder chats and append last message
    //but I don't have time
    this.updateChats();
  }

  onNewOnlineOfflineUser(userId: number, isOnline: boolean) {
    var userFilter = this.chats.filter(a => a.userId == userId);
    if (userFilter.length == 0)
      return;
    if (userFilter[0].isOnline == isOnline) {
      return;
    }
    userFilter[0].isOnline = isOnline;
    this.chats = [...this.chats];
  }

  private updateChats() {
    var act = this.activeUserChat?.userId;
    this.userChatService
      .getUserChatsWithGroups()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res) => {
          this.chats = res;
          if (act != null) {
            this.setActive(this.chats.filter((a) => a.userId == act)[0]);
          }
        }
      });
  }
  private getUsers() {
    this.applicationUserService
      .getUsers()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res) => {
          this.users = res;
        }
      });
  }
  private isUserExists(a: GetUserDto): boolean {
    return this.chats.filter(c => c.userId == a.id).length > 0;
  }
  private userAsChatUserObject(element: GetUserDto): GetGroupDto {
    return {
      id: 0,
      isGroup: false,
      isOnline: element.isOnline,
      lastMessage: null,
      lastSeen: null,
      members: [],
      name: element.name,
      userId: element.id,
      userImage: element.imageUrl
    };
  }
}

