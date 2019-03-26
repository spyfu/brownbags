export const akkaHeirarchy = `
    graph TD;
        /;
        /user;
        /system;
        /topLevelActor1
        /topLevelActor2
        /childActor1
        /childActor2
        /-->/user;
        /-->/system;
        /user-->/topLevelActor1;
        /user-->/topLevelActor2;
        /topLevelActor1 --> /childActor1;
        /topLevelActor1 --> /childActor2;
`;