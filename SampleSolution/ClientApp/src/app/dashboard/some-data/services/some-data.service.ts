import { Injectable, Inject } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import { DeleteDto } from "../models/deleteDto";
import { CreateDto } from "../models/createDto";
import { SomeData } from "../../../shared/data-obj/models/some-data";

@Injectable()
export class SomeDataService {

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string) {
  }

  getSampleSolution(): Observable<Array<SomeData>> {
    let headers = this.buildHeaders();
    return this.http.get<SomeData[]>(this.baseUrl + 'api/SomeData', { headers });
  }

  delete(deleteDto: DeleteDto): Observable<{}> {
    let headers = this.buildHeaders();
    return this.http.delete(this.baseUrl + 'api/SomeData/' + deleteDto.id, { headers });
  }

  create(createDto: CreateDto): Observable<{}> {
    let headers = this.buildHeaders();
    return this.http.post(this.baseUrl + 'api/SomeData', createDto, { headers });
  }

  edit(editDto: SomeData): Observable<{}> {
    let headers = this.buildHeaders();
    return this.http.put(this.baseUrl + 'api/SomeData', editDto, { headers });
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
