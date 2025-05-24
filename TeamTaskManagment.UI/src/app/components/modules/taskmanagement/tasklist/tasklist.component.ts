import { Component, ElementRef, OnDestroy, ViewChild, OnInit, AfterViewInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { MatPaginator, MatSort, MatTableDataSource } from '@angular/material';
import { MatDialog, MatDialogRef } from '@angular/material';

import { DOCUMENT } from '@angular/common';
import { Inject } from '@angular/core';
import { TranslateService, LangChangeEvent } from '@ngx-translate/core';
import { DataService } from 'src/app/services/data.service';
import { GlobalFunctionsService } from 'src/app/services/global/global-functions.service';
import { ConfirmDialogType } from 'src/app/models/global';
import { TaskItem } from 'src/app/Models/models.model';
import { ConfirmationDialogComponent } from 'src/app/components/widgets/confirmation-dialog/confirmation-dialog.component';
import { DatePipe } from '@angular/common';
@Component({
  selector: 'app-tasklist',
  templateUrl: './tasklist.component.html',
  styleUrls: ['./tasklist.component.css']
})
export class TasklistComponent implements OnInit, OnDestroy, AfterViewInit {


  //#region variables
  @ViewChild('epltable', { static: false }) epltable: ElementRef;
  displayedColumns: string[] = ['code', 'tittle', 'priority', 'status', 'assignedTo', 'Action'];
  dataSource: MatTableDataSource<TaskItem>;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild('frame', { static: false }) public modal: any;
  compNativeElement: any;
  formitem: FormGroup;
  priortydata;
  statusdata;
  statusid;
  priorityid;
  submitted = false;
  edit = false;
  AddressBookData: TaskItem[] = [];
  confirmationDialogRef: MatDialogRef<ConfirmationDialogComponent>;
  //#endregion
  //#region constructor
  constructor(

    private fb: FormBuilder,
    private el: ElementRef,
    public dialog: MatDialog,
    private router: Router,
    public translate: TranslateService,
    public gfs: GlobalFunctionsService,
    public ds: DataService,
    private dt: DatePipe,
    @Inject(DOCUMENT) private document: Document
  ) {
    this.dataSource = new MatTableDataSource();
    this.compNativeElement = this.el.nativeElement;
  }




  //#endregion
  //#region Actions
  addnew() {
    this.submitted = false;
    this.edit = false;
    this.formitem.reset();
    this.getpriortydata();
    this.getstatusdata();
    this.showModal();
  }
  showModal() {
    this.modal.show();
  }
  getdatabyid(id: any) {

    this.getDataById(id).subscribe((res: any) => {
      if (res) {
        const data = res.result;
        debugger;
        this.formitem.patchValue({
          id: data.id,
          title: data.title,
          description: data.description,
          priorityId: data.priorityId,
          statusId: data.statusId,
          dueDate: this.dt.transform(data.dueDate, "yyyy-MM-dd"),
          assignedTo: data.assignedTo
        });
      }
    });
    this.edit = true;
    this.showModal();
  }
  getalldata() {
    this.dataSource.data = [] as TaskItem[];
    this.getData().subscribe((res: any) => {
      if (res) {
        this.dataSource.data = res.result;
      }

    });

  }
  getpriortydata() {

    this.getpriorty().subscribe((res: any) => {
      if (res) {
        this.priortydata = res.result;
      }

    });

  }
  getstatusdata() {

    this.getstatus().subscribe((res: any) => {
      if (res) {
        this.statusdata = res.result;
      }

    });

  }
  selectstatusvalue(e) {
    this.statusid = e.target.value;
  }
  selectedpriorityvalue(e) {
    this.priorityid = e.target.value;
  }
  FilterData(): void {
    let assignto = this.compNativeElement.querySelector('#assigendto').value;
    if (this.statusid == null) { this.statusid = 0; }
    this.gfs.subscriptions.push(

      this.searchdata(this.statusid, assignto).subscribe((res) => {
        if (res) {
          this.dataSource.data = res;
        }
        else {
          this.dataSource.data = [];
        }
      }, () => {

      }
      ));
  }
  saveaction() {

    this.onSubmit().then((res) => {
      if (res) {
        const saveObj = Object.assign({}, this.formitem.getRawValue());

        if (!this.edit) {
          this.gfs.subscriptions.push(

            this.postData(saveObj).subscribe((response: { success: any, id: any }) => {
              this.SuccessProcess();

            }, (err) => {
              debugger;
              console.log(err.error.errors);
              this.FaildProcess(err.error.errors);

            }));
        }
        else {
          this.gfs.subscriptions.push(

            this.putData(saveObj).subscribe((response: { success: any, id: any }) => {
              this.SuccessProcess();

            }, (err) => {
              this.FaildProcess(err);
            }));
        }
      }
    });

  }
  deleteaction(id: any) {
    this.confirmationDialogRef = (this.dialog.open(ConfirmationDialogComponent, {
      data: { type: ConfirmDialogType.Delete, msg: this.translate.instant('MSGConfirmDelete') },
    })) as MatDialogRef<ConfirmationDialogComponent, any>;
    this.confirmationDialogRef.afterClosed().subscribe((result) => {
      if (result) {


        this.gfs.subscriptions.push(
          this.delete(id).subscribe((response: { success: any, id: any }) => {
            this.SuccessProcess();


          }, (err) => {
            this.FaildProcess(err);
          }));
      }
    });
  }

  SuccessProcess() {
    this.gfs.openShowMessageDialog((this.translate.instant('MSGSuccess')), '');
    this.formitem.reset();
    this.getalldata();
    this.modal.hide();
  }
  FaildProcess(err) {
    this.gfs.openShowMessageDialog((this.translate.instant('MSGFail')) + err, '');
    this.modal.hide();
  }

  //#endregion
  // #region  Functions

  async onSubmit(): Promise<boolean> {
    this.submitted = true;
    if (this.formitem.invalid) {
      return;
    }
    const obj = Object.assign({}, this.formitem.getRawValue());
    return obj;
  }
  get f() {
    return this.formitem.controls;
  }
  CancelFunc() {
    this.formitem.reset({});
    this.modal.hide();
  }
  ngOnDestroy(): void {
    // ... Mandatory
    this.gfs.disposed();
  }
  ngOnInit(): void {

    this.getalldata();
    this.getpriortydata();
    this.getstatusdata();
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
    this.compNativeElement = this.el.nativeElement;
    this.formitem = this.fb.group({
      id: [],
      title: ['', [Validators.required]],
      description: [''],
      priorityId: ['', [Validators.required]],
      priorityName: [''],
      statusId: ['', Validators.required],
      statusName: [''],
      dueDate: ['', Validators.required],
      assignedTo: ['']
    });

  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  //#endregion

  //#region Private Functions

  getpriorty(): Observable<any> {
    return this.ds.get('LookupItems/GetPriority');
  }
  getstatus(): Observable<any> {
    return this.ds.get('LookupItems/GetStatus');
  }
  getDataById(id): Observable<any> {
    const result = this.ds.getWithParams('Tasks/GetTaskDataById', { 'id': id });
    return result;
  }
  getData(): Observable<any> {
    return this.ds.get('Tasks/GetTaskData');
  }
  searchdata(status: any, name: any): Observable<any> {
    const result = this.ds.getWithParams('Tasks/FilterData', {
      'name': name,
      'status': status
    });
    return result;
  }
  postData(data): Observable<any> {
    return this.ds.post('Tasks/Create', data);
  }
  putData(data): Observable<any> {
    return this.ds.put('Tasks/Edit', data);
  }
  delete(id): Observable<any> {
    debugger;
    return this.ds.deleteWithParams('Tasks/Delete', { 'id': id });
  }
  //#endregion



}
