CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE OR REPLACE function fn_get_secret_key()
RETURNS TEXT
LANGUAGE plpgsql
AS $$
BEGIN
	RETURN 'sopwqmfsoasdls';
END;
$$;

/* 1. Create a stored procedure to encrypt a given text
Task: Write a stored procedure sp_encrypt_text that takes a plain text input (e.g., email or mobile number) and returns an encrypted version using PostgreSQL's pgcrypto extension.
 
Use pgp_sym_encrypt(text, key) from pgcrypto. */

CREATE OR REPLACE PROCEDURE sp_encrypt_text(
	p_email TEXT,
	OUT o_encrypted_email bytea 
)
LANGUAGE plpgsql
AS $$
DECLARE
	secret_key TEXT := fn_get_secret_key();
BEGIN
	SELECT pgp_sym_encrypt(p_email, secret_key) INTO o_encrypted_email;
END;
$$;

DO $$
DECLARE 
	v_encrypted_email bytea;
BEGIN
	CALL sp_encrypt_text('password',v_encrypted_email);
	RAISE NOTICE '%', v_encrypted_email;
END;
$$;

/* 2. Create a stored procedure to compare two encrypted texts
Task: Write a procedure sp_compare_encrypted that takes two encrypted values and checks if they decrypt to the same plain text. */

CREATE OR REPLACE PROCEDURE sp_compare_encrypted(
	p_encrypted_text1 bytea,
	p_encrypted_text2 bytea,
	OUT o_are_same BOOLEAN
)
LANGUAGE plpgsql
AS $$
DECLARE
	p_decrypted_text1 text;
	p_decrypted_text2 text;
	secret_key TEXT := fn_get_secret_key();
BEGIN
	SELECT pgp_sym_decrypt(p_encrypted_text1,secret_key) INTO p_decrypted_text1;
	SELECT pgp_sym_decrypt(p_encrypted_text2,secret_key) INTO p_decrypted_text2;
	IF (p_decrypted_text1 = p_decrypted_text2) THEN o_are_same := true;
	ELSE o_are_same := false;
	END IF;
END;
$$;

DO $$
DECLARE 
	v_encrypted_email1 bytea;
	v_encrypted_email2 bytea;
	v_are_same BOOLEAN;
BEGIN
	CALL sp_encrypt_text('pard',v_encrypted_email1);
	CALL sp_encrypt_text('password',v_encrypted_email2);
	CALL sp_compare_encrypted(v_encrypted_email1,v_encrypted_email2,v_are_same);
	RAISE NOTICE '%',v_are_same;
END;
$$;

/* 3. Create a stored procedure to partially mask a given text
Task: Write a procedure sp_mask_text that:
 
Shows only the first 2 and last 2 characters of the input string
 
Masks the rest with *
 
E.g., input: 'john.doe@example.com' → output: 'jo***************om' */

CREATE OR REPLACE PROCEDURE sp_mask_text(
	p_input_text TEXT,
	OUT o_masked_text TEXT
)
LANGUAGE plpgsql
AS $$
BEGIN
	o_masked_text := REGEXP_REPLACE(p_input_text, '(\S{2})\S*(\S{2})', '\1**********\2');
end;
$$;

DO $$
DECLARE 
	v_masked_text TEXT;
BEGIN
	CALL sp_mask_text('john.doe@example.com',v_masked_text);
	RAISE NOTICE '%',v_masked_text;
END;
$$;

/* 4. Create a procedure to insert into customer with encrypted email and masked name
Task:
 
Call sp_encrypt_text for email
 
Call sp_mask_text for first_name
 
Insert masked and encrypted values into the customer table
 
Use any valid address_id and store_id to satisfy FK constraints. */

CREATE TABLE customer(
	customer_id SERIAL primary key,
	first_name VARCHAR(50),
	last_name VARCHAR(50),
	email bytea
);

CREATE OR REPLACE PROCEDURE sp_insert_customer(
	p_first_name VARCHAR(50),
	p_last_name VARCHAR(50),
	p_email TEXT
)
LANGUAGE plpgsql
AS $$
DECLARE
	v_encrypted_email bytea;
	v_masked_first_name TEXT;
BEGIN
	CALL sp_encrypt_text(p_email,v_encrypted_email);
	SELECT fn_mask_text(p_first_name) INTO v_masked_first_name;
	INSERT INTO customer (first_name, last_name, email) VALUES (v_masked_first_name,p_last_name,v_encrypted_email);
END;
$$;

CALL sp_insert_customer('John','Doe','johndoe@gmail.com');

SELECT * FROM customer;

/* 5. Create a procedure to fetch and display masked first_name and decrypted email for all customers
Task:
Write sp_read_customer_masked() that:
 
Loops through all rows
 
Decrypts email
 
Displays customer_id, masked first name, and decrypted email */

create or replace procedure sp_read_customer_masked()
LANGUAGE plpgsql
AS $$
DECLARE
	secret_key TEXT := fn_get_secret_key();
	customer_cursor cursor for
	SELECT customer_id, first_name, pgp_sym_decrypt(email,secret_key) as "email" from customer;
	customer_record record;
BEGIN
	OPEN customer_cursor;
	LOOP
		FETCH customer_cursor INTO customer_record;
		EXIT WHEN NOT FOUND;
		RAISE notice 'ID: %, First Name: %, Email: %',customer_record.customer_id,customer_record.first_name,customer_record.email;
	END LOOP;
	CLOSE customer_cursor;
END;
$$;

call sp_read_customer_masked()