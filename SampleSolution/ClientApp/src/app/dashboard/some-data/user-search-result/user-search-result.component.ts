import { Component, OnInit, Input } from '@angular/core';
import { StreamResult } from "../models/streamResult";
import { SomeDataService } from "../services/some-data.service";

@Component({
  selector: 'app-user-search-result',
  templateUrl: './user-search-result.component.html',
  styleUrls: ['./user-search-result.component.css']
})
export class UserSearchResultComponent implements OnInit {
  @Input()
  result: StreamResult;

  @Input()
  contactId: string;

  constructor(private someDataService: SomeDataService) { }

  ngOnInit() {
  }

  sendClick() {
    console.log(this.contactId + ", " + this.result.userId);
    this.someDataService.share(this.contactId, this.result.userId).subscribe(result => {
        location.reload();
      },
      error => console.error(error));
  }

}
