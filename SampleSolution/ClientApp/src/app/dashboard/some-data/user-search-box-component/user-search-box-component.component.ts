import { Component, OnInit, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import { StreamService } from '../services/stream.service';
import { StreamResult } from "../models/streamResult";
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/fromEvent';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/switch';
import 'rxjs/add/operator/debounceTime';

@Component({
  selector: 'app-user-search-box-component',
  templateUrl: './user-search-box-component.component.html',
  styleUrls: ['./user-search-box-component.component.css']
})
export class UserSearchBoxComponentComponent implements OnInit {

  constructor(private streamService: StreamService,
    private el:ElementRef) { }

  @Output()
  loading: EventEmitter<boolean> = new EventEmitter<boolean>();

  @Output()
  results: EventEmitter<StreamResult[]> = new EventEmitter<StreamResult[]>();

  ngOnInit() {
    Observable.fromEvent(this.el.nativeElement, 'keyup')
      .map((e: any) => e.target.value)
      .filter((text: string) => {
        if (text.length > 0) {
          return true;
        } else {
          this.results.emit(null);
          return false;
        }
      })
      .debounceTime(250)
      .do(() => this.loading.emit(true))
      .map((query: string) => this.streamService.search(query))
      .switch()
      .subscribe((results: StreamResult[]) => {
          console.log(results);
          this.loading.emit(false);
          this.results.emit(results);
        },
        (err: any) => {
          console.log(err);
          this.loading.emit(false);
        },
        () => {
          this.loading.emit(false);
        });
  }

}
