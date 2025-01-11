import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { GetGroupDto } from '../../../models/chats/addGroupDto';
import { GetUserChatMessageDto } from '../../../models/chats/userChatDto';
import { ChatUserMessageTemplateComponent } from "./chat-user-message-template/chat-user-message-template.component";
import { FormatDatePipe } from '../../../pipes/format-date.pipe';

@Component({
  standalone: true,
  selector: 'app-chat-box',
  imports: [ChatUserMessageTemplateComponent, FormatDatePipe],
  templateUrl: './chat-box.component.html',
  styleUrl: './chat-box.component.css'
})
export class ChatBoxComponent {
  @Input()
  chatMessages: GetUserChatMessageDto[] = [];
  @Input()
  activeUserChat?: GetGroupDto;
  @ViewChild('scrollableDiv')
  scrollableDiv!: ElementRef<HTMLDivElement>;

  scrollToEnd() {
    if (this.scrollableDiv) {
      setTimeout(() => {
        //why timeout ? to let html bind first then scrolling
        const div = this.scrollableDiv.nativeElement;
        div.scrollTop = div.scrollHeight;
      }, 50);
    }
  }
}
