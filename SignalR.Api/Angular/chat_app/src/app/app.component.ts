import { AfterViewInit, Component, OnInit } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { SignalRService } from './core/services/common/signalr.service';
import { AuthenticateService } from './core/services/auth/authenticate.service';
import { CommonModule } from '@angular/common';
import { ApplicationUserService } from './core/services/auth/applicationUser.service';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { BaseComponent } from './core/features/common/basComponent';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent extends BaseComponent {
  currentYear = new Date().getFullYear();
  constructor(private authenticateService: AuthenticateService,
    private userService: ApplicationUserService,
    private router: Router) { super(); }

  public get isUserAuthenticate(): boolean {
    return this.authenticateService.isUserAuthenticated();
  }

  public get fullName(): string | null {
    return this.authenticateService.getFullName();
  }

  logOut() {
    this.userService.signOut()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: () => {
          //
        }
      })
    this.authenticateService.removeAuthentication();
    this.router.navigate(['/login']);
  }
}
