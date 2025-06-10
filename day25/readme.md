Case Study 21: Online Classroom Attendance
Roles: Teacher, Student
Features:
Mark attendance for live sessions
View attendance history
SignalR for real-time attendance updates and notifications
JWT-based role segregation

Services:

Authentication Service:
- Login (generate token)
- Logout (invalidate token)

Teacher Service:
- Create Teacher
- Deactivate Teacher

Student Service:
- Create Student
- Deactivate Student

Session Service:
- Schedule session
- Cancel Session
- Complete Session
- Update Session
- Get all sessions
- get sessions by teacher
- get sessions for student
- add students to session
- remove students from sessions

Attendance Service:
- Add attendance to student
- Add attendace to students
- Remove attendance to student
- Remove attendace to students
- Get attendance of a student
- Get attendance of students of a session
