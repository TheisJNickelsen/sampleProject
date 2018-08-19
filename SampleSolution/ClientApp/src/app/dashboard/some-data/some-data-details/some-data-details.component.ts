import { Component, OnInit, Inject, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ActivatedRoute, Router, Params } from '@angular/router';
import 'rxjs/add/operator/map'
import { DeleteDto } from '../models/deleteDto';
import { Subscription } from 'rxjs/Subscription';
import { SomeDataService } from '../services/some-data.service';
import { CreateDto } from "../models/createDto";
import { MessageService } from "../../../shared/services/message-service/message.service";
import { SomeData } from "../../../shared/data-obj/models/some-data";

@Component({
  selector: 'app-some-data-details',
  templateUrl: './some-data-details.component.html',
  styleUrls: ['./some-data-details.component.css']
})
export class SomeDataDetailsComponent implements OnInit, OnDestroy {
  subscription: Subscription;

  isEdit: boolean;

  pageTitle: string;
  submitBtnText: string;

  firstName: string;
  middleName: string;
  lastName: string;
  title: string;
  colorCode: string;
  facebookUrl: string;
  dataId: string;

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService,
    private someDataService: SomeDataService) {

    this.baseUrl = baseUrl;

    this.route.data.subscribe(data => {
      this.isEdit = data["isEdit"];
      if (this.isEdit) {
        this.pageTitle = 'Edit data';
        this.submitBtnText = 'Save changes';
      } else {
        this.pageTitle = 'Create new data entry';
        this.submitBtnText = 'Create data entry';
      }
    });
  }

  ngOnInit() {
    this.subscription = this.messageService.getSelectedEntry().subscribe(data => {
      if (this.isEdit && data != null) {
        this.firstName = data.firstName;
        this.middleName = data.middleName;
        this.lastName = data.lastName;
        this.title = data.title;
        this.colorCode = data.color;
        this.facebookUrl = data.facebookUrl;
        this.dataId = data.id;
      }
    });
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  submit() {
    if (this.isEdit) {
      let entry = new SomeData();
      entry.id = this.dataId;
      entry.firstName = this.firstName;
      entry.middleName = this.middleName;
      entry.lastName = this.lastName;
      entry.title = this.title;
      entry.color = this.colorCode;
      entry.facebookUrl = this.facebookUrl;
      this.someDataService.edit(entry).subscribe(result => {
        },
        error => {
          console.log(error);
        });
    } else {
      let entry = new CreateDto();
      entry.firstName = this.firstName;
      entry.middleName = this.middleName;
      entry.lastName = this.lastName;
      entry.title = this.title;
      entry.color = this.colorCode;
      entry.facebookUrl = this.facebookUrl;

      this.someDataService.create(entry).subscribe(result => {
          this.router.navigate(['dashboard/my-data']);
        },
        error => {
          console.log(error);
        });
    }
  }

  delete() {
    let deleteDto = new DeleteDto();
    deleteDto.id = this.dataId;
    this.someDataService.delete(deleteDto).subscribe(result => {
      console.log(result);
      this.router.navigate(['dashboard/my-data']);
      },
      error => console.error(error));

  }
}
