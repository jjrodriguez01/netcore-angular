import { HttpClient } from '@angular/common/http';
import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MdbModalRef } from 'mdb-angular-ui-kit/modal';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-timeslip-edit',
  templateUrl: './timeslip-edit.component.html',
  styleUrls: ['./timeslip-edit.component.css']
})
export class TimeslipEditComponent implements OnInit {

  id: number | null = null;
  departments: Observable<Department[]> | undefined;
  tasks: MyTask[] = [];
  task: MyTask | undefined;
  timeslip: Timeslip | undefined;
  departmentId = 1;
  taskId =1;
  form = new FormGroup({
    department: new FormControl({}),
    task: new FormControl({}),
    comment: new FormControl(''),
    date: new FormControl(''),
    time: new FormControl(''),
    dt: new FormControl('')
  });
  http: HttpClient;
  url: string;
  constructor(public modalRef: MdbModalRef<TimeslipEditComponent>,http: HttpClient, @Inject('API_URL') baseUrl: string) { 
    this.http=http;
    this.url=baseUrl;
  }

  ngOnInit(): void {
    //mover a un servicio si hay tiempo
    //traer ptos
    this.departments = this.http.get<Department[]>(this.url + 'department')
    .pipe(
      //catchError(this.handleError)
    );
    //traer el ts
    this.http.get<Timeslip>(this.url + 'Timeslip/'+this.id).subscribe({
      next: (v) => 
      {
        console.log(v);
        this.timeslip = v;
        //traer el depto de la task asociada
        //hay q hacerlo porque se debe dar la opcion de cambiar a otras task del mismo depto
        this.http.get<MyTask>(this.url + 'Task/'+this.timeslip.taskId).subscribe({
          next: (v) => 
          {
            this.task = v;
            this.departmentId = this.task.departmentId;
            this.taskId = this.task.taskId;
          },
          error: (e) => {console.error(e); this.tasks=[]},//si error limpiar select
          complete: () => console.info('complete') 
        });
        this.http.get<MyTask[]>(this.url + 'Task/by-department/'+this.departmentId).subscribe({
          next: (v) => 
          {
            this.tasks = v;
          },
          error: (e) => {console.error(e); this.tasks=[]},//si error limpiar select
          complete: () => console.info('complete') 
        });
        this.form.controls['comment'].setValue(this.timeslip.comment);
        this.form.controls['department'].setValue(this.departmentId);
        this.form.controls['task'].setValue(this.taskId);
        var newDate = new Date(this.timeslip.dateTime);
        console.log(newDate.toISOString().split('T')[0]);
        this.form.controls['date'].setValue(this.timeslip.dateTime.split('T')[0]);//se separa la date de time y se toma la parte date

      },
      error: (e) => console.error(e),
      complete: () => console.info('complete') 
    });
    
  }

   onChangeDepto(e: any){
    this.http.get<MyTask[]>(this.url + 'Task/by-department/'+e.target.value).subscribe({
      next: (v) => 
      {
        console.log(v);
        this.tasks = v;
      },
      error: (e) => {console.error(e); this.tasks=[]},//si error limpiar select
      complete: () => console.info('complete') 
    });
  }
}
//deberian ir en un modelo para evitar duplicarlas
interface Timeslip {
  timeslipId: number;
  dateTime: string;
  taskId: string;
  comment: string;
  applicationUserId: string;
}

interface Department {
  departmentId: number;
  name: string;
}

interface MyTask {
  taskId: number;
  name: string;
  departmentId: number;
}
