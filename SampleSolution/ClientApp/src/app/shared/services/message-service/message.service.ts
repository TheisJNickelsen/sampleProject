import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import { SomeData } from "../../data-obj/models/some-data";

@Injectable()
export class MessageService {
  private entryToEdit = new BehaviorSubject<SomeData>(null);

  selectedEntry(entry: SomeData) {
    this.entryToEdit.next(entry);
  }

  getSelectedEntry(): Observable<any> {
    return this.entryToEdit.asObservable();
  }
}
