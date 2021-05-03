import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShellComponent } from './components/shell/shell.component';
import { HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { AllComponent } from './components/all/all.component';
import { ByGardenComponent } from './components/by-garden/by-garden.component';
import { TableViewComponent } from './components/table-view/table-view.component';
import { MatTableModule } from "@angular/material/table";
import { MatButtonModule } from "@angular/material/button";
import { MatIconModule } from "@angular/material/icon";
import { MatButtonToggleModule } from "@angular/material/button-toggle";
import { MatProgressBarModule } from "@angular/material/progress-bar";
import { MatSnackBarModule } from "@angular/material/snack-bar";

@NgModule({
  declarations: [ShellComponent, AllComponent, ByGardenComponent, TableViewComponent],
  imports: [
    CommonModule,
    HttpClientModule,
    RouterModule.forChild([
      {
        path: 'top10',
        component: ShellComponent,
        children: [
          {path: 'all', component: AllComponent},
          {path: 'byGarden', component: ByGardenComponent}
        ]
      }
    ]),
    MatTableModule,
    MatButtonModule,
    MatIconModule,
    MatButtonToggleModule,
    MatProgressBarModule,
    MatSnackBarModule,
  ],
  exports: [ShellComponent]
})
export class Top10Module { }
