import { Component, OnInit,Inject, ViewChild } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { MdbModalRef, MdbModalService } from 'mdb-angular-ui-kit/modal';
import { TimeslipEditComponent } from '../timeslip-edit/timeslip-edit.component';

@Component({
  selector: 'app-timeslip',
  templateUrl: './timeslip.component.html',
  styleUrls: ['./timeslip.component.css']
})
export class TimeslipComponent implements OnInit {

  //@ViewChild('modal') modal: any;
  modalRef: MdbModalRef<TimeslipEditComponent> | null = null;
  public url : string = "";
  public timeslips: Timeslip[] = [];
  public departments: Observable<Department[]> | undefined;
  public tasks: MyTask[] = [];
  http: HttpClient;
  form = new FormGroup({
    department: new FormControl({}),
    task: new FormControl({}),
    comment: new FormControl(''),
    date: new FormControl(''),
    time: new FormControl(''),
    dt: new FormControl('')
  });
  
  constructor(http: HttpClient, @Inject('API_URL') baseUrl: string,private fb: FormBuilder,private modalService: MdbModalService) 
  {
    this.url = baseUrl;
    this.http = http;
  }

  //al cambiar
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

  eliminar(id: number){
    console.log(id);
    this.http.delete(this.url+'timeslip/'+id)
      .subscribe({next: (v) => 
        {
          console.log(v);
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete') });
        window.location.reload();
  }

  editar(id: number){
    console.log(id);
    this.modalRef = this.modalService.open(TimeslipEditComponent,{data: {id: id}});
  }

  onCreateSubmit(){
    const BODY = {taskId:Number(this.form.controls['task'].value),
    dateTime: this.form.controls['date'].value + 'T'+
    this.form.controls['time'].value,
    comment: this.form.controls['comment'].value,
    userId: '48cc318f-72a8-421c-9a1e-304611071331'//sacar el userId del token
    };
    if(isNaN(BODY.taskId)){//no selecciono taskid
      return;//se deberia informar de mejor manera
    }
    console.log(BODY);
    //mover a servicio si hay tiempo
    this.http.post(this.url+'timeslip',BODY)
      .subscribe({next: (v) => 
        {
          console.log(v);
        },
        error: (e) => console.error(e),
        complete: () => console.info('complete') });
        //para refrescar tabla
        // this.http.get<Timeslip[]>(this.url + 'Timeslip').subscribe({
        //   next: (v) => 
        //   {
        //     console.log(v);
        //     this.timeslips = v;
        //   },
        //   error: (e) => console.error(e),
        //   complete: () => console.info('complete') 
        // });
        window.location.reload();//mirar por que no funciono el refresco de la variable para evitar esto
  }

  ngOnInit(): void {
    //mover a un servicio si hay tiempo
    this.http.get<Timeslip[]>(this.url + 'Timeslip').subscribe({
      next: (v) => 
      {
        console.log(v);
        this.timeslips = v;
      },
      error: (e) => console.error(e),
      complete: () => console.info('complete') 
    });
    this.departments = this.http.get<Department[]>(this.url + 'department')
    .pipe(
      //catchError(this.handleError)
    );
    
    
  }

}

interface Timeslip {
  timeslipId: number;
  dateTime: string;
  userName: string;
  comment: string;
  taskName: string;
  departmentName : string;
  shortDate : string;
  hour : string;
}

interface Department {
  departmentId: number;
  name: string;
}

interface MyTask {
  taskId: number;
  name: string;
}
