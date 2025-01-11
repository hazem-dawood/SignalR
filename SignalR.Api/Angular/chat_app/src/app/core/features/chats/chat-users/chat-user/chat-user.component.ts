import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GetGroupDto } from '../../../../models/chats/addGroupDto';
import { CommonModule } from '@angular/common';
import { FormatDatePipe } from "../../../../pipes/format-date.pipe";

@Component({
  selector: 'app-chat-user',
  imports: [CommonModule, FormatDatePipe],
  templateUrl: './chat-user.component.html',
  styleUrl: './chat-user.component.css'
})
export class ChatUserComponent {
  @Input()
  chat!: GetGroupDto;
  @Output()
  setActiveToParent = new EventEmitter<GetGroupDto>();
  @Input()
  activeUserChat?: GetGroupDto;

  setActive(chat: GetGroupDto) {
    this.setActiveToParent.emit(chat);
  }
}
