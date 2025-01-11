import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GetGroupDto } from '../../../../models/chats/addGroupDto';


@Component({
  selector: 'app-chat-group',
  imports: [],
  templateUrl: './chat-group.component.html',
  styleUrl: './chat-group.component.css'
})
export class ChatGroupComponent {
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
