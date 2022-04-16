-- This script was generated by a beta version of the ERD tool in pgAdmin 4.
-- Please log an issue at https://redmine.postgresql.org/projects/pgadmin4/issues/new if you find any bugs, including reproduction steps.
BEGIN;


CREATE TABLE IF NOT EXISTS "ACCOUNT"."GROUPS"
(
    "ID_GROUP" bigserial NOT NULL,
    "NAME" character varying(120) NOT NULL,
    "DESC" character varying(255),
    CONSTRAINT "PK_GROUPS" PRIMARY KEY ("ID_GROUP")
);

CREATE TABLE IF NOT EXISTS "ACCOUNT"."GROUP_ROLES"
(
    "ID" bigserial NOT NULL,
    "ID_GROUP" bigint NOT NULL,
    "ID_ROLE" bigint NOT NULL,
    CONSTRAINT "PK_GROUP_ROLES" PRIMARY KEY ("ID")
);

CREATE TABLE IF NOT EXISTS "ACCOUNT"."MODULES"
(
    "ID_MODULE" bigserial NOT NULL,
    "NAME" character varying(120) NOT NULL,
    "DESC" character varying(255),
    CONSTRAINT "PK_MODULES" PRIMARY KEY ("ID_MODULE")
);

CREATE TABLE IF NOT EXISTS "ACCOUNT"."ROLES"
(
    "ID_ROLE" bigserial NOT NULL,
    "NAME" character varying(120) NOT NULL,
    "DESC" character varying(255),
    CONSTRAINT "PK_ROLES" PRIMARY KEY ("ID_ROLE")
);

CREATE TABLE IF NOT EXISTS "ACCOUNT"."ROLE_MODULES"
(
    "ID" bigserial NOT NULL,
    "ID_ROLE" bigint NOT NULL,
    "ID_MODULE" bigint NOT NULL,
    CONSTRAINT "PK_ROLE_MODULES" PRIMARY KEY ("ID")
);

CREATE TABLE IF NOT EXISTS "ACCOUNT"."USERS"
(
    "ID_USER" bigserial NOT NULL,
    "LOGIN" character varying(20) NOT NULL,
    "PASS" character varying(32) NOT NULL,
    "IS_ACTIVE" "char" NOT NULL,
    "NAME" character varying(120) NOT NULL,
    "DESC" character varying(255),
    "PASS_DATE" date,
    CONSTRAINT "PK_USERS" PRIMARY KEY ("ID_USER")
);

CREATE TABLE IF NOT EXISTS "ACCOUNT"."USER_AUTHS"
(
    "ID_AUTH" bigserial NOT NULL,
    "ID_USER" bigint NOT NULL,
    "ID_ACTION" integer NOT NULL,
    "STAMP" timestamp without time zone NOT NULL,
    "SYSTEM" character varying(255) NOT NULL,
    "MESSAGE" character varying(100),
    CONSTRAINT "PK_USER_AUTHS" PRIMARY KEY ("ID_AUTH")
);

CREATE TABLE IF NOT EXISTS "ACCOUNT"."USER_GROUPS"
(
    "ID" bigserial NOT NULL,
    "ID_USER" bigint NOT NULL,
    "ID_GROUP" bigint NOT NULL,
    CONSTRAINT "PK_USER_GROUPS" PRIMARY KEY ("ID")
);

CREATE TABLE IF NOT EXISTS "ACCOUNT"."USER_ROLES"
(
    "ID" bigserial NOT NULL,
    "ID_USER" bigint NOT NULL,
    "ID_ROLE" bigint NOT NULL,
    CONSTRAINT "PK_USER_ROLES" PRIMARY KEY ("ID")
);

CREATE TABLE IF NOT EXISTS "DICT"."REQUEST_STATES"
(
    "ID_STATE" serial NOT NULL,
    "STATE" character varying(120) NOT NULL,
    "DESC" character varying(255),
    CONSTRAINT "PK_REQUEST_STATES" PRIMARY KEY ("ID_STATE")
);

CREATE TABLE IF NOT EXISTS "DICT"."REQUEST_TYPES"
(
    "ID_TYPE" serial NOT NULL,
    "TYPE" character varying(120) NOT NULL,
    "DESC" character varying(255),
    CONSTRAINT "PK_REQUEST_TYPES" PRIMARY KEY ("ID_TYPE")
);

CREATE TABLE IF NOT EXISTS "MQ"."REQUESTS"
(
    "ID_REQUEST" bigserial NOT NULL,
    "ID_USER" bigint NOT NULL,
    "ID_TYPE" integer NOT NULL,
    "ID_STATE" integer NOT NULL,
    "JSON" character varying(500),
    "GUID" character varying(100),
    "CREATED" timestamp without time zone NOT NULL,
    "PROCESSED" timestamp without time zone,
    "DELIVERED" timestamp without time zone,
    "MESSAGE" character varying(500),
    CONSTRAINT "PK_REQUESTS" PRIMARY KEY ("ID_REQUEST")
);

CREATE TABLE IF NOT EXISTS "DICT"."AUTH_ACTIONS"
(
    "ID_ACTION" serial NOT NULL,
    "ACTION" character varying(120) NOT NULL,
    "DESC" character varying(255),
    CONSTRAINT "PK_AUTH_ACTIONS" PRIMARY KEY ("ID_ACTION")
);

ALTER TABLE IF EXISTS "ACCOUNT"."GROUP_ROLES"
    ADD CONSTRAINT "FK_GROUP_ROLES_GROUP" FOREIGN KEY ("ID_GROUP")
    REFERENCES "ACCOUNT"."GROUPS" ("ID_GROUP") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "ACCOUNT"."GROUP_ROLES"
    ADD CONSTRAINT "FK_GROUP_ROLES_ROLE" FOREIGN KEY ("ID_ROLE")
    REFERENCES "ACCOUNT"."ROLES" ("ID_ROLE") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "ACCOUNT"."ROLE_MODULES"
    ADD CONSTRAINT "FK_ROLE_MODULES_ROLE" FOREIGN KEY ("ID_ROLE")
    REFERENCES "ACCOUNT"."ROLES" ("ID_ROLE") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "ACCOUNT"."ROLE_MODULES"
    ADD CONSTRAINT "FK_ROLE_MODULES_MODULE" FOREIGN KEY ("ID_MODULE")
    REFERENCES "ACCOUNT"."MODULES" ("ID_MODULE") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE NO ACTION
    NOT VALID;


ALTER TABLE IF EXISTS "ACCOUNT"."USER_AUTHS"
    ADD CONSTRAINT "FK_USER_AUTHS_USER" FOREIGN KEY ("ID_USER")
    REFERENCES "ACCOUNT"."USERS" ("ID_USER") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "ACCOUNT"."USER_AUTHS"
    ADD CONSTRAINT "FK_USER_AUTHS_AUTH_ACTION" FOREIGN KEY ("ID_ACTION")
    REFERENCES "DICT"."AUTH_ACTIONS" ("ID_ACTION") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "ACCOUNT"."USER_GROUPS"
    ADD CONSTRAINT "FK_USER_GROUPS_USER" FOREIGN KEY ("ID_USER")
    REFERENCES "ACCOUNT"."USERS" ("ID_USER") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "ACCOUNT"."USER_GROUPS"
    ADD CONSTRAINT "FK_USER_GROUPS_GROUP" FOREIGN KEY ("ID_GROUP")
    REFERENCES "ACCOUNT"."GROUPS" ("ID_GROUP") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "ACCOUNT"."USER_ROLES"
    ADD CONSTRAINT "FK_USER_ROLES_USER" FOREIGN KEY ("ID_USER")
    REFERENCES "ACCOUNT"."USERS" ("ID_USER") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "ACCOUNT"."USER_ROLES"
    ADD CONSTRAINT "FK_USER_ROLES_ROLE" FOREIGN KEY ("ID_ROLE")
    REFERENCES "ACCOUNT"."ROLES" ("ID_ROLE") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "MQ"."REQUESTS"
    ADD CONSTRAINT "FK_REQUESTS_USER" FOREIGN KEY ("ID_USER")
    REFERENCES "ACCOUNT"."USERS" ("ID_USER") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "MQ"."REQUESTS"
    ADD CONSTRAINT "FK_REQUESTS_REQUEST_TYPE" FOREIGN KEY ("ID_TYPE")
    REFERENCES "DICT"."REQUEST_TYPES" ("ID_TYPE") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;


ALTER TABLE IF EXISTS "MQ"."REQUESTS"
    ADD CONSTRAINT "FK_REQUESTS_REQUEST_STATE" FOREIGN KEY ("ID_STATE")
    REFERENCES "DICT"."REQUEST_STATES" ("ID_STATE") MATCH SIMPLE
    ON UPDATE NO ACTION
    ON DELETE CASCADE
    NOT VALID;

END;