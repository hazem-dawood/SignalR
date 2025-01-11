import { IdNameDto } from "./idNameDto"
import { IUserOnlineDto } from "./iUserOnlineDto"

export interface GetUserDto extends IdNameDto, IUserOnlineDto {
    imageUrl: string
    userName: string
    isOnline: boolean
    lastSeen: string | null
    token: string | null
}