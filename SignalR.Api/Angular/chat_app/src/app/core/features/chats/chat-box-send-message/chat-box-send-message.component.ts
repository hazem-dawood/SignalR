import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GetGroupDto } from '../../../models/chats/addGroupDto';
import { FormsModule } from '@angular/forms';
import { BaseComponent } from '../../common/basComponent';
import { UserChatMessageService } from '../../../services/chats/userChatMessage.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { GetUserChatMessageDto } from '../../../models/chats/userChatDto';

@Component({
  standalone: true,
  selector: 'app-chat-box-send-message',
  imports: [FormsModule],
  templateUrl: './chat-box-send-message.component.html',
  styleUrl: './chat-box-send-message.component.css'
})
export class ChatBoxSendMessageComponent extends BaseComponent {
  @Output()
  onMessageAdded = new EventEmitter<GetUserChatMessageDto>();
  constructor(private userChatMessageService: UserChatMessageService) {
    super();
  }
  @Input()
  activeUserChat?: GetGroupDto;
  message: string = '';
  sendMessage() {
    if (!!!this.message) {
      return;
    }

    this.userChatMessageService
      .add({
        message: this.message,
        toUserId: this.activeUserChat!.userId,
        userChatId: this.activeUserChat!.id
      })
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.onMessageAdded.emit({
              createdDate: new Date().toDateString(),
              from: '',
              isFromMe: true,
              isSeen: true,
              message: this.message,
              to: ''
            });
            this.message = '';
          }
        }
      })
  }
}
