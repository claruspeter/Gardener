# Gardener

![Alt text](https://g.gravizo.com/svg?digraph G {
    edge[fontsize=12];
    App[label="Seequent App"] ;
    TenDuke[label="10Duke",shape=rect,style=filled];
    Dynamo[shape=record,label="|Dynamo stream"];
    IdpProcessor;
    ResolveQueue[shape=record,label="|Resolve Queue"];
    Resolver;
    EventQueue[shape=record,label="|Event Queue"];
    EventProcessor;
    Database[shape=Mrecord,label="|Database|"];
    Api;
    MonitorPortal[shape=box3d];
    OneMinute[shape=none];
    App->TenDuke[label="checkout licence"];
    TenDuke->Dynamo[label="event"];
    Dynamo->IdpProcessor[label=trigger];
    IdpProcessor->EventQueue[label="rename events"];
    IdpProcessor->ResolveQueue[label="consumption events"];
    OneMinute->Resolver[style=dotted,label=tick];
    ResolveQueue->Resolver[label="read"];
    Resolver->TenDuke[dir="both",label="query for\n org/group"];
    Resolver->EventQueue[label="resolved\nconsumption"];
    OneMinute->EventProcessor[style=dotted,label=tick];
    EventQueue->EventProcessor[label="read"];
    EventProcessor->Database[label=update];
    Database->Api[label="query"];
    Api->MonitorPortal[label=display];
    }
  }
)
