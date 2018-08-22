import { Component, OnInit, Input } from '@angular/core';
import { StreamResult } from "../models/streamResult";

@Component({
  selector: 'app-user-search-result',
  templateUrl: './user-search-result.component.html',
  styleUrls: ['./user-search-result.component.css']
})
export class UserSearchResultComponent implements OnInit {
  @Input()
  result:StreamResult;

  constructor() { }

  ngOnInit() {
  }

}
