/* Phase 2 - DDL & DML */
-- Create all tables with appropriate constraints (PK, FK, UNIQUE, NOT NULL)
CREATE TABLE Student (
	student_id SERIAL PRIMARY KEY,
	name VARCHAR(100) NOT NULL,
	email VARCHAR(100) NOT NULL,
	phone VARCHAR(12) NOT NULL
);

CREATE TABLE Course (
	course_id SERIAL PRIMARY KEY,
	course_name VARCHAR(100) NOT NULL,
	category VARCHAR(50) NOT NULL,
	duration_days INT NOT NULL
);

CREATE TABLE Trainer (
	trainer_id SERIAL PRIMARY KEY,
	trainer_name VARCHAR(100) NOT NULL,
	expertise VARCHAR(50) NOT NULL
);

CREATE TABLE Enrollment (
	enrollment_id SERIAL PRIMARY KEY,
	student_id INT  NOT NULL,
	course_id INT  NOT NULL,
	enroll_date DATE,
	UNIQUE(student_id, course_id),
	FOREIGN KEY (student_id) REFERENCES Student (student_id),
	FOREIGN KEY (course_id) REFERENCES Course (course_id)
);

CREATE TABLE Certificate(
	certificate_id SERIAL PRIMARY KEY,
	enrollment_id INT NOT NULL UNIQUE,
	issue_date DATE,
	serial_no VARCHAR(12) UNIQUE,
	FOREIGN KEY (enrollment_id) REFERENCES Enrollment (enrollment_id)
);

CREATE TABLE Course_Trainer (
	course_id INT NOT NULL,
	trainer_id INT NOT NULL,
	PRIMARY KEY(course_id,trainer_id)
);

-- Insert sample data using `INSERT` statements
INSERT INTO Student (name, email, phone) VALUES
('Alice Johnson', 'alice.johnson@example.com', '1234567890'),
('Bob Smith', 'bob.smith@example.com', '2345678901'),
('Charlie Lee', 'charlie.lee@example.com', '3456789012');

INSERT INTO Course (course_name, category, duration_days) VALUES
('Python Programming', 'Programming', 30),
('Data Science Basics', 'Data Science', 45),
('Web Development', 'Web', 40);

INSERT INTO Trainer (trainer_name, expertise) VALUES
('Dr. Emily Carter', 'Data Science'),
('John Doe', 'Programming'),
('Sarah Kim', 'Web Development');

INSERT INTO Enrollment (student_id, course_id, enroll_date) VALUES
(1, 1, '2025-01-10'),
(2, 2, '2025-02-15'),
(3, 3, '2025-03-20');

CREATE OR REPLACE FUNCTION fn_generate_certificate_serial()
RETURNS VARCHAR(12)
LANGUAGE plpgsql
AS $$
BEGIN
	Return Substring(REPLACE(gen_random_uuid()::TEXT, '-', '') FROM 0 FOR 12);
END;
$$;

INSERT INTO Certificate (enrollment_id, issue_date, serial_no) VALUES
(1, '2025-02-10', fn_generate_certificate_serial()),
(2, '2025-03-20', fn_generate_certificate_serial()),
(3, '2025-04-25', fn_generate_certificate_serial());

INSERT INTO Course_Trainer (course_id, trainer_id) VALUES
(1, 2),  
(2, 1),  
(3, 3);

SELECT * FROM Student;
SELECT * FROM Course;
SELECT * FROM Trainer;
SELECT * FROM Enrollment;
SELECT * FROM Certificate;
SELECT * FROM Course_Trainer;

-- Create indexes on `student_id`, `email`, and `course_id`
CREATE INDEX student_id_idx ON Student (student_id);
CREATE INDEX email_idx ON Student (email);
CREATE INDEX course_id_idx ON Course (course_id);

/* Phase 3 - SQL Joins Practice */
-- List students and the courses they enrolled in
SELECT name, course_name FROM Student
JOIN Enrollment ON Enrollment.student_id = Student.student_id
Join Course ON Course.course_id = Enrollment.course_id;

-- Show students who received certificates with trainer names
SELECT name, course_name, serial_no, issue_date FROM Certificate
JOIN Enrollment ON Enrollment.enrollment_id = Certificate.enrollment_id
JOIN Student ON Student.student_id = Enrollment.student_id
JOIN Course ON Course.course_id = Enrollment.course_id;

-- Count number of students per course
SELECT course_name, COUNT(*) "number_of_enrollment" FROM Course
LEFT JOIN Enrollment on Course.course_id = Enrollment.course_id
GROUP BY course_name;

/* Phase 4: Functions & Stored Procedures */
-- Create `get_certified_students(course_id INT)`
-- → Returns a list of students who completed the given course and received certificates.

CREATE OR REPLACE FUNCTION fn_get_certified_students(
	p_course_id INT
)
RETURNS TABLE(student_name VARCHAR(100))
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN QUERY
	SELECT name student_name FROM Student
	JOIN Enrollment ON Enrollment.student_id = Student.student_id
	JOIN Certificate ON Certificate.enrollment_id = Enrollment.enrollment_id
	WHERE course_id = p_course_id;
END;
$$;

SELECT * FROM fn_get_certified_students(1);

-- Create `sp_enroll_student(p_student_id, p_course_id)`
-- → Inserts into `enrollments` and conditionally adds a certificate if completed (simulate with status flag).

CREATE OR REPLACE PROCEDURE sp_enroll_student(
	p_student_id INT,
	p_course_id INT,
	p_completed_status BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
	v_course_duration INT;
	v_enrollment_date DATE;
	v_enrollment_id INT;
BEGIN
	SELECT duration_days INTO v_course_duration FROM Course 
	WHERE course_id = p_course_id;
	
	IF (p_completed_status IS TRUE) 
	THEN v_enrollment_date := NOW() - FORMAT('%s days',v_course_duration)::INTERVAL;
	ELSE v_enrollment_date := NOW();
	END IF;
	
	INSERT INTO Enrollment (student_id, course_id, enroll_date)
	VALUES (p_student_id,p_course_id,v_enrollment_date)
	RETURNING enrollment_id INTO v_enrollment_id;
	
	IF (p_completed_status IS TRUE)
	THEN INSERT INTO Certificate (enrollment_id, issue_date, serial_no)
	VALUES (v_enrollment_id,NOW(),fn_generate_certificate_serial());
	END IF;
END;
$$;

CALL sp_enroll_student(1,3,false);
SELECT * FROM Enrollment ORDER BY enrollment_id DESC;
SELECT * FROM Certificate ORDER BY certificate_id DESC;

CALL sp_enroll_student(2,1,true);
SELECT * FROM Enrollment ORDER BY enrollment_id DESC;
SELECT * FROM Certificate ORDER BY certificate_id DESC;

/* Phase 5: Cursor */
-- Loop through all students in a course
CREATE OR REPLACE PROCEDURE sp_students_in_course(
	p_course_id INT
)
LANGUAGE plpgsql
AS $$
DECLARE
	student_cursor CURSOR FOR
	SELECT name from Student
	JOIN Enrollment on Enrollment.student_id = Student.student_id
	WHERE course_id = p_course_id;
	
	student_record record;
BEGIN
	OPEN student_cursor;
	LOOP
		FETCH student_cursor INTO student_record;
		EXIT WHEN NOT FOUND;

		RAISE NOTICE 'Student: %',student_record.name;
	END LOOP;
	CLOSE student_cursor;
END;
$$;

CALL sp_students_in_course(1);

-- Print name and email of those who do not yet have certificates
CREATE OR REPLACE PROCEDURE sp_students_without_certificate()
LANGUAGE plpgsql
AS $$
DECLARE
	student_cursor CURSOR FOR
	SELECT name,email from Student
	WHERE student_id NOT IN (SELECT DISTINCT student_id FROM Enrollment);
	
	student_record record;
BEGIN
	OPEN student_cursor;
	LOOP
		FETCH student_cursor INTO student_record;
		EXIT WHEN NOT FOUND;

		RAISE NOTICE 'Student: %, Email: %',student_record.name,student_record.email;
	END LOOP;
	CLOSE student_cursor;
END;
$$;

CALL sp_students_without_certificate();

/* Phase 6: Security & Roles */
-- Create a `readonly_user` role:
--    * Can run `SELECT` on `students`, `courses`, and `certificates`
--    * Cannot `INSERT`, `UPDATE`, or `DELETE`

CREATE ROLE readonly_user LOGIN PASSWORD 'readonly_user';
GRANT SELECT ON Student, Course, Certificate TO readonly_user;

-- Create a `data_entry_user` role:
--    * Can `INSERT` into `students`, `enrollments`
--    * Cannot modify certificates directly

CREATE ROLE data_entry_user LOGIN PASSWORD 'data_entry_user';
GRANT INSERT ON Student, Enrollment TO data_entry_user;

/* Phase 7: Transactions & Atomicity */
-- Write a transaction block that:
-- * Enrolls a student
-- * Issues a certificate
-- * Fails if certificate generation fails (rollback)

CREATE OR REPLACE PROCEDURE sp_enroll_and_issue_certificate(
	p_student_id INT,
	p_course_id INT
)
LANGUAGE plpgsql
AS $$
DECLARE
	v_course_duration INT;
	v_enrollment_date DATE;
	v_enrollment_id INT;
BEGIN
	BEGIN
		SELECT duration_days INTO v_course_duration FROM Course 
		WHERE course_id = p_course_id;
		v_enrollment_date := NOW() - FORMAT('%s days',v_course_duration)::INTERVAL;

		INSERT INTO Enrollment (student_id, course_id, enroll_date)
		VALUES (p_student_id,p_course_id,v_enrollment_date)
		RETURNING enrollment_id INTO v_enrollment_id;

		INSERT INTO Certificate (enrollment_id, issue_date, serial_no)
		VALUES (v_enrollment_id,NOW(),fn_generate_certificate_serial());	
	END;
	EXCEPTION WHEN OTHERS THEN
		RAISE EXCEPTION 'Error: Unable to enroll and issue certificate';
		ROLLBACK;
END;
$$;

CALL sp_enroll_and_issue_certificate(1,2);
CALL sp_enroll_and_issue_certificate(1,2);

SELECT * FROM Enrollment;