import { Component, OnInit, Input, ChangeDetectorRef, OnChanges, SimpleChanges, AfterViewInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
declare function treeviewInit(): any;

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  childUserName = '';
  currentUser;
  menuData = [];
  menuvalidadmin;
  menuvalidsupervisor;
  private URL = './assets/config/menus.json';
  constructor(
    public translate: TranslateService,
    private http: HttpClient,
    private router: Router) {
  }

  ngOnInit() {
    this.getDataFile();
  }


  private getDataFile() {
    console.log(this.URL);
    return this.http.get(this.URL).toPromise().then((res: { menus: any }) => {
      this.menuData = (Object as any).values(res.menus);

    });

  }
  ngAfterViewInit(): void {
    treeviewInit();

  }

  routerlink(query) {
    this.router.navigate([query[0]])
      .then(() => {
        window.location.reload();
      });
  }






}
