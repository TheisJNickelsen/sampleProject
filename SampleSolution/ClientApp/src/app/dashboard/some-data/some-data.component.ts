import { Component, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { SomeData } from "../../shared/data-obj/models/some-data";
import { MessageService } from "../../shared/services/message-service/message.service";
import { SomeDataService } from "./services/some-data.service";
import { DeleteDto } from './models/deleteDto';
import { StreamService } from "./services/stream.service";
import { StreamResult } from "./models/streamResult";

@Component({
  selector: 'some-data',
  templateUrl: './some-data.component.html',
  styleUrls: ['./some-data.component.css']
})
export class SomeDataComponent {
  public someData: SomeData[];

  results: StreamResult[];

  constructor(private router: Router,
    private messageService: MessageService,
    private someDataService: SomeDataService,
    private streamService: StreamService) {

    someDataService.getSampleSolution().subscribe(result => {
      console.log(result);
      this.someData = result;
      },
      error => console.error(error));
  }

  createClick() {
    this.router.navigate(['dashboard/create-some-data']);
  }


  editClick(entry: SomeData) {
    this.router.navigate(['dashboard/edit-some-data']);
    this.messageService.selectedEntry(entry);
  }

  deleteClick(entry: SomeData) {
    let deleteDto = new DeleteDto();
    deleteDto.id = entry.id;
    this.someDataService.delete(deleteDto).subscribe(result => {
      location.reload();
    },
      error => console.error(error));
  }

  updateResults(results: StreamResult[]): void {
    this.results = results;
  }
}


