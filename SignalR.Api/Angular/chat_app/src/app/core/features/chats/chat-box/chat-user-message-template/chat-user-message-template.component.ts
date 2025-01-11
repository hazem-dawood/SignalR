import { Component, Input } from '@angular/core';
import { GetUserChatMessageDto } from '../../../../models/chats/userChatDto';
import { GetGroupDto } from '../../../../models/chats/addGroupDto';
import { FormatDatePipe } from "../../../../pipes/format-date.pipe";

@Component({
  standalone: true,
  selector: 'app-chat-user-message-template',
  imports: [FormatDatePipe],
  templateUrl: './chat-user-message-template.component.html',
  styleUrl: './chat-user-message-template.component.css'
})
export class ChatUserMessageTemplateComponent {
  @Input()
  message!: GetUserChatMessageDto;
  @Input()
  activeUserChat?: GetGroupDto;
}
