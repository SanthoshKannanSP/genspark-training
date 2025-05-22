# Day 09 - May 15th, 2025
## Session Overview
- Encryption
- Masking
- pgCrypto
- Host-Based Authentication
- Access Control

## Encryption
- converting readable plaintext into unreadable cipher text
- protect sensitive data from unauthorized access
- without encryption key, the data cannot be understood

## Masking
- obscuring sensitive data so that unauthorized users cannot view the actual values, but can still use the data in a limited way
- used in test environments, analytics and UIs with restrcited access
- **Static Masking:** The data is stored as masked. Used in copies of a database. All users view the masked data.
- **Dynamic Masking:** The data is stored unmasked. The data is masked on-the-fly based on user permisiions.
- **EG:**
```
Original: johnsmith@gmail.com
Masked: jo*****th@gmail.com
```

## pgCrypto
[Reference](https://www.postgresql.org/docs/current/pgcrypto.html)
- PostgreSQL module that provides cryptographic functions
- Enable pgcrypto
```
CREATE EXTENSION IF NOT EXISTS pgcrypto;
```
- Symmetric Key Encryption
```
SELECT pgp_sym_encrypt(<plaintext>,<secret_key>);
SELECT pgp_sym_decrypt(<encrypted_text>,<secret_key>);
```
- Hashing
```
/* Using Digest */
SELECT digest(<text_data>, <hashing_algorithm>);
/* Using Mac - prevents tampering as secret key should be known to generate hash */
SELECT hmac(<text_data,<key>,<hashing_algorithm>);
```
- Password Hashing
```
SELECT crypt(<plaintext_password>, gen_salt(<salting_algorithm>));
/* Passwords are stored as hashes and not as plaintext */
/* To autheticate entered password*/
SELECT crypt(<entered_password>, <stored_hash>) = <stored_hash>;
```
## Host-based authentication (pg_hba.conf)
- used to control how, where and with what authetication method that users can connect to a PostgreSQL database
- `pg_hba.conf` file can be found in PostgreSQl's data folder
- Each line in `pg_hba.conf` follows the format
```
TYPE DATABASE USER ADDRESS METHOD [OPTIONS]
```
- **TYPE:** Connection type (*local*, *host*, *hostssl*, *hostnossl*)
- **DATABASE:** Which database(s) the rule applies to (*all*, a database name, *replication*)
- **USER:** PostgreSQL username(s)
- **ADDRESS:** IP address range for remote connections
- **METHOD:** Authetication method (*trust*, *md5*, *scram-sha-256*, *ident*)
- **OPTIONS:** Extra parameters for some methods (*clientcert=1*)

## Access Control
[Reference](https://www.postgresql.org/docs/current/ddl-priv.html)
- When a database object is created, the role that created the object will have the ownership of the object
- Only the owner (or a superuser) can do anything with the object. To allow other roles to use the object, priviledges must be granted
- To create a role
```
CREATE ROLE <rolename> LOGIN PASSWORD '<password>';
```
- To grant priveledges ([Reference](https://www.postgresql.org/docs/current/sql-grant.html))
```
GRANT <priveledges> ON <object> TO <role>;
```
- To remove priveledges ([Reference](https://www.postgresql.org/docs/current/sql-revoke.html))
```
REVOKE <priveledges> ON <object> FROM <role>;
```
## Other References
- https://superuser.com/questions/1226649/why-does-postgresqls-pgp-sym-encrypt-return-different-output-although-invoked-f
- https://stackoverflow.com/questions/29122667/searching-encrypted-field-in-postgres
- https://neon.tech/postgresql/postgresql-string-functions/regexp_replace
- https://stackoverflow.com/questions/27709456/what-is-the-difference-between-a-user-and-a-role