import { Contact } from "./contact.model";
import { Role } from "./role.model";

export interface User {
    id: number;
    firstName: string;
    lastName?: string;
    active: boolean;
    company: string;
    sex: string;
    roleId: number;
    contact: Contact;
    role: Role;
}