import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ApplicationUserService } from '../../../services/auth/applicationUser.service';
import { BaseComponent } from '../../common/basComponent';
import { takeUntilDestroyed } from "@angular/core/rxjs-interop";
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GetUserDto } from '../../../models/common/getUserDto';
import { CommonModule } from '@angular/common';
import { AuthenticateService } from '../../../services/auth/authenticate.service';

@Component({
  standalone: true,
  selector: 'app-log-in',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './log-in.component.html',
  styleUrl: './log-in.component.css'
})
export class LogInComponent extends BaseComponent implements OnInit {
  usersObservable!: Observable<GetUserDto[]>;

  constructor(private formBuilder: FormBuilder,
    private applicationUserService: ApplicationUserService,
    private router: Router,
    private authenticateService: AuthenticateService) {
    super();
  }
  formGroup!: FormGroup;
  ngOnInit(): void {
    this.formGroup = this.formBuilder.group({
      userName: ['hazem', [Validators.required]],
      password: ['123456', [Validators.required]]
    });
    this.usersObservable = this.applicationUserService.getUsers();
  }

  logIn() {
    if (!this.formGroup.valid)
      return;
    this.applicationUserService.signIn(this.formGroup.value)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (res) => {
          if (res.isSuccess) {
            this.authenticateService.authenticateUser(res.data!);
            this.router.navigate(['/home']);
          } else {
            //
          }
        },
        error: (err) => {

        },
        complete: () => {

        }
      });
  }
}