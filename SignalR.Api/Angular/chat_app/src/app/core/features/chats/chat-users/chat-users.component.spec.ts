import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChatUsersComponent } from './chat-users.component';

describe('ChatUsersComponent', () => {
  let component: ChatUsersComponent;
  let fixture: ComponentFixture<ChatUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ChatUsersComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ChatUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
