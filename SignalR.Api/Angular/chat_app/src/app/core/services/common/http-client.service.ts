import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: 'root',
})
export class HttpClientService {
  static domain: string = 'https://localhost:7129/';
  static _baseUrl: string = `${HttpClientService.domain}api/`;
  constructor(private _httpClient: HttpClient) { }

  get<T>(url: string): Observable<T> {
    return this._httpClient.get<T>(HttpClientService._baseUrl + url);
  }

  post<T>(url: string, model: any): Observable<T> {
    return this._httpClient.post<T>(HttpClientService._baseUrl + url, model);
  }
}