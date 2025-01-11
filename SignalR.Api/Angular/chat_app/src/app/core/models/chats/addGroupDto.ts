import { GetUserDto } from "../common/getUserDto"
import { IdNameDto } from "../common/idNameDto"
import { IUserOnlineDto } from "../common/iUserOnlineDto"
import { LastMessageDto } from "../common/lastMessageDto"
import { RequestPaginationDto } from "../common/requestPaginationDto"

export interface AddGroupDto {
    name: string
    userIds: number[]
}

export interface SendGroupMessageDto {
    groupId: number
    message: string
}

export interface EditGroupDto extends AddGroupDto {
    id: number
}

export interface GetGroupDto extends IdNameDto, IUserOnlineDto {
    members: GetUserDto[]
    lastMessage: LastMessageDto | null
    isGroup: boolean
    userId: number
    userImage: string
    isOnline: boolean
    lastSeen: string | null
}

export interface RequestGroupMessageDto extends RequestPaginationDto {
    groupId: number
}