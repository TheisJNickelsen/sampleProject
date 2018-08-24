import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { StreamResult } from "../models/streamResult";

@Injectable()
export class StreamService {
  private limit: number = 10;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) { }

  search(query: string) {
    let headers = this.buildHeaders();
    return this.http.get<StreamResult[]>(this.baseUrl + 'api/User/search?query=' + query + '&limit=' + this.limit, { headers });
  }

  private buildHeaders(): HttpHeaders {
    let authToken = localStorage.getItem('auth_token');

    let headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + authToken
    });

    return headers;
  }

}
