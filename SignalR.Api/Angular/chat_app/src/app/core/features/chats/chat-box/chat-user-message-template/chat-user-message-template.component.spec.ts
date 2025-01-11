import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatUserMessageTemplateComponent } from './chat-user-message-template.component';

describe('ChatUserMessageTemplateComponent', () => {
  let component: ChatUserMessageTemplateComponent;
  let fixture: ComponentFixture<ChatUserMessageTemplateComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatUserMessageTemplateComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatUserMessageTemplateComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
