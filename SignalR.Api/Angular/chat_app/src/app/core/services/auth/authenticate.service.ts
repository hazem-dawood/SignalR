import { isPlatformBrowser } from "@angular/common";
import { Inject, Injectable, PLATFORM_ID } from "@angular/core";
import { GetUserDto } from "../../models/common/getUserDto";

@Injectable({
    providedIn: 'root'
})
export class AuthenticateService {
    constructor(@Inject(PLATFORM_ID) private platformId: Object) { }
    getFullName(): string | null {
        return window.localStorage.getItem('fullName');
    }
    getLastAntToken(): string | null {
        if (isPlatformBrowser(this.platformId)) {
            return window.localStorage.getItem('antiToken');
        }
        return null;
    }
    setLastAntToken(to: string | null) {
        if (isPlatformBrowser(this.platformId)) {
            return window.localStorage.setItem('antiToken', to ?? '');
        }
    }
    getToken(): string {
        if (isPlatformBrowser(this.platformId)) {
            return window.localStorage.getItem('token')!;
        } return '';
    }

    isUserAuthenticated(): boolean {
        if (isPlatformBrowser(this.platformId)) {
            return window.localStorage.getItem('token') != null
        }
        return false;
    }

    authenticateUser(user: GetUserDto): boolean {
        if (isPlatformBrowser(this.platformId)) {
            window.localStorage.setItem('fullName', user.name)
            return window.localStorage.setItem('token', user.token!) != null;
        }
        return false;
    }

    removeAuthentication() {
        if (isPlatformBrowser(this.platformId)) {
            window.localStorage.removeItem('token');
            window.localStorage.removeItem('fullName');
        }
    }
}