import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Student } from './models/student.model';



@Injectable({
  providedIn: 'root'
})
export class StudentService {

  private apiUrl = '/api/students';

  constructor(private http: HttpClient) { }

  getAllStudents(): Observable<Student[]> {
    return this.http.get<Student[]>(this.apiUrl);
  }

  getStudentById(id: number): Observable<Student> {
    return this.http.get<Student>(`${this.apiUrl}/${id}`);
  }

  addStudent(student: Student): Observable<Student> {
    return this.http.post<Student>(this.apiUrl, student);
  }

  updateStudent(student: Student): Observable<Student> {
    return this.http.put<Student>(`${this.apiUrl}/${student.id}`, student);
  }

  deleteStudent(id: number): Observable<Student> {
    return this.http.delete<Student>(`${this.apiUrl}/${id}`);
  }
}
