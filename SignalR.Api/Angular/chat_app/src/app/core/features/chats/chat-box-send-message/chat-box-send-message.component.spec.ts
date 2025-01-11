import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatBoxSendMessageComponent } from './chat-box-send-message.component';

describe('ChatBoxSendMessageComponent', () => {
  let component: ChatBoxSendMessageComponent;
  let fixture: ComponentFixture<ChatBoxSendMessageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatBoxSendMessageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatBoxSendMessageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
